using AutoMapper;
using Demo.DataTransmitModel.Student;
using Demo.Domain;
using Demo.Domain.Repositories;
using Demo.EFRepository;
using Demo.Service;
using Demo.Service.Model.Student;
using Materal.ConvertHelper;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Demo.Domain.Repositories.Views;
using Demo.Domain.Views;

namespace Demo.ServiceImpl
{
    public class StudentServiceImpl : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentInfoViewRepository _studentInfoViewRepository;
        private readonly IDemoUnitOfWork _demoUnitOfWork;
        private readonly IMapper _mapper;
        public StudentServiceImpl(IStudentRepository studentRepository, IMapper mapper, IDemoUnitOfWork demoUnitOfWork, IStudentInfoViewRepository studentInfoViewRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _demoUnitOfWork = demoUnitOfWork;
            _studentInfoViewRepository = studentInfoViewRepository;
        }
        public async Task AddStudentAsync(AddStudentModel model)
        {
            if(string.IsNullOrWhiteSpace(model.Name)) throw new InvalidOperationException("名称不能为空");
            if (model.Name.Length > 100) throw new InvalidOperationException("名称不能超过100个字符");
            var student = model.CopyProperties<Student>();
            _demoUnitOfWork.RegisterAdd(student);
            await _demoUnitOfWork.CommitAsync();
        }
        public async Task EditStudentAsync(EditStudentModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name)) throw new InvalidOperationException("名称不能为空");
            if (model.Name.Length > 100) throw new InvalidOperationException("名称不能超过100个字符");
            Student studentFromDB = await _studentRepository.FirstOrDefaultAsync(model.ID);
            if (studentFromDB == null) throw new InvalidOperationException("学生不存在");
            model.CopyProperties(studentFromDB);
            studentFromDB.UpdateTime = DateTime.Now;
            _demoUnitOfWork.RegisterEdit(studentFromDB);
            await _demoUnitOfWork.CommitAsync();
        }
        public async Task DeleteStudentAsync(Guid id)
        {
            Student studentFromDB = await _studentRepository.FirstOrDefaultAsync(id);
            if (studentFromDB == null) throw new InvalidOperationException("学生不存在");
            _demoUnitOfWork.RegisterDelete(studentFromDB);
            await _demoUnitOfWork.CommitAsync();
        }
        public async Task<StudentDTO> GetStudentInfoAsync(Guid id)
        {
            Student student = await _studentRepository.FirstOrDefaultFromSubordinateAsync(id);
            var result = _mapper.Map<StudentDTO>(student);
            return result;
        }
        public async Task<(List<StudentListDTO> studentInfo, PageModel pageModel)> GetStudentListAsync(QueryStudentFilterModel filterModel)
        {
            if (filterModel.MinAge.HasValue && filterModel.MaxAge.HasValue && filterModel.MinAge.Value > filterModel.MaxAge.Value) throw new InvalidOperationException("最小年龄必须小于最大年龄");
            (List<StudentInfoView> studentList, PageModel pageModel) = await _studentInfoViewRepository.PagingFromSubordinateAsync(filterModel, m => m.UpdateTime, SortOrder.Descending);
            var result = _mapper.Map<List<StudentListDTO>>(studentList);
            return (result, pageModel);
        }
    }
}
