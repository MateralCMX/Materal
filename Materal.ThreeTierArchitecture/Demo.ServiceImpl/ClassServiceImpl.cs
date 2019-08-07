using AutoMapper;
using Demo.DataTransmitModel.Class;
using Demo.Domain;
using Demo.Domain.Repositories;
using Demo.EFRepository;
using Demo.Service;
using Demo.Service.Model.Class;
using Materal.ConvertHelper;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Demo.ServiceImpl
{
    public class ClassServiceImpl : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IDemoUnitOfWork _demoUnitOfWork;
        private readonly IMapper _mapper;
        public ClassServiceImpl(IClassRepository classRepository, IStudentRepository studentRepository, IMapper mapper, IDemoUnitOfWork demoUnitOfWork)
        {
            _classRepository = classRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _demoUnitOfWork = demoUnitOfWork;
        }
        public async Task AddClassAsync(AddClassModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name)) throw new InvalidOperationException("名称不能为空");
            if (model.Name.Length > 100) throw new InvalidOperationException("名称不能超过100个字符");
            var @class = model.CopyProperties<Class>();
            _demoUnitOfWork.RegisterAdd(@class);
            await _demoUnitOfWork.CommitAsync();
        }
        public async Task EditClassAsync(EditClassModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name)) throw new InvalidOperationException("名称不能为空");
            if (model.Name.Length > 100) throw new InvalidOperationException("名称不能超过100个字符");
            Class classFromDB = await _classRepository.FirstOrDefaultAsync(model.ID);
            if (classFromDB == null) throw new InvalidOperationException("班级不存在");
            model.CopyProperties(classFromDB);
            classFromDB.UpdateTime = DateTime.Now;
            _demoUnitOfWork.RegisterEdit(classFromDB);
            await _demoUnitOfWork.CommitAsync();
        }
        public async Task DeleteClassAsync(Guid id)
        {
            Class classFromDB = await _classRepository.FirstOrDefaultAsync(id);
            if (classFromDB == null) throw new InvalidOperationException("班级不存在");
            List<Student> studentsFromDB = await _studentRepository.FindAsync(m => m.BelongClassID == id);
            Parallel.ForEach(studentsFromDB, student => _demoUnitOfWork.RegisterDelete(student));
            _demoUnitOfWork.RegisterDelete(classFromDB);
            await _demoUnitOfWork.CommitAsync();
        }
        public async Task<ClassDTO> GetClassInfoAsync(Guid id)
        {
            Class @class = await _classRepository.FirstOrDefaultFromSubordinateAsync(id);
            var result = _mapper.Map<ClassDTO>(@class);
            return result;
        }
        public async Task<(List<ClassListDTO> classInfo, PageModel pageModel)> GetClassListAsync(QueryClassFilterModel filterModel)
        {
            (List<Class> classList, PageModel pageModel) = await _classRepository.PagingFromSubordinateAsync(filterModel, m => m.UpdateTime, SortOrder.Descending);
            var result = _mapper.Map<List<ClassListDTO>>(classList);
            return (result, pageModel);
        }
    }
}
