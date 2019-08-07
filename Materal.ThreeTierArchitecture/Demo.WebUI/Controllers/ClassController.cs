using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Demo.DataTransmitModel.Class;
using Demo.PresentationModel.Class.Request;
using Demo.PresentationModel.Other;
using Demo.Service;
using Demo.Service.Model.Class;
using Materal.Model;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebUI.Controllers
{
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly IMapper _mapper;
        public ClassController(IClassService classService, IMapper mapper)
        {
            _classService = classService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> List(QueryClassFilterRequestModel filterModel)
        {
            var model = _mapper.Map<QueryClassFilterModel>(filterModel);
            (List<ClassListDTO> classInfo, PageModel pageModel) = await _classService.GetClassListAsync(model);
            ViewData["FilterModel"] = filterModel;
            ViewData["PageModel"] = pageModel;
            return View(classInfo);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddClassRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<AddClassModel>(requestModel);
                await _classService.AddClassAsync(model);
                return Redirect("/Class/List");
            }
            catch (InvalidOperationException ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = ex.Message
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ClassDTO classInfo = await _classService.GetClassInfoAsync(id);
            return View(classInfo);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditClassRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<EditClassModel>(requestModel);
                await _classService.EditClassAsync(model);
                return Redirect("/Class/List");
            }
            catch (InvalidOperationException ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = ex.Message
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _classService.DeleteClassAsync(id);
                return Redirect("/Class/List");
            }
            catch (InvalidOperationException ex)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = ex.Message
                });
            }
        }
    }
}