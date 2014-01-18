using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using SmartHealth.Box.Domain.Dtos;
using SmartHealth.Box.Domain.Models;
using SmartHealth.Core.Domain.Dtos;
using SmartHealth.Core.Domain.Models;
using SmartHealth.Infrastructure.Bussiness;
using SmartHealth.Web.Controllers;

namespace SmartHealth.Web.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DocumentLogController : BaseController
    {
        private readonly IService<DocumentLog> documentLogService;

        public DocumentLogController(IService<DocumentLog> documentLogService)
        {
            this.documentLogService = documentLogService;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Administrator/Views/DocumentLog/Index.cshtml");
        }

        public ActionResult GetDocumentLogs()
        {
            var documentLogs =
                documentLogService.GetAll().OrderByDescending(a => a.CreatedDate).Select(
                    documentLog => new DocumentLogDto
                                   {
                                       Id = documentLog.Id,
                                       CreatedDate = documentLog.CreatedDate,
                                       Email = documentLog.Email,
                                       Document = Mapper.Map<Document, DocumentDto>(documentLog.Document)
                                   });
            return Json(documentLogs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateOrUpdateDocumentLog(string models)
        {
            var documentLogDto =
                JsonConvert.DeserializeObject<List<DocumentLogDto>>(models).FirstOrDefault();
            DocumentLog documentLog = Mapper.Map<DocumentLogDto, DocumentLog>(documentLogDto);
            documentLogService.SaveOrUpdate(documentLog, true);
            return Json(Mapper.Map<DocumentLog, DocumentLogDto>(documentLog), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteDocumentLogs(string ids)
        {
            foreach (DocumentLog documentLog in ids.Split(',').Select(id => documentLogService.Get(Convert.ToInt32(id))))
            {
                documentLogService.Delete(documentLog, true);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
