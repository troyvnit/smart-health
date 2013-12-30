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
    public class MediaLogController : BaseController
    {
        private readonly IService<MediaLog> mediaLogService;

        public MediaLogController(IService<MediaLog> mediaLogService)
        {
            this.mediaLogService = mediaLogService;
        }

        public ActionResult Index()
        {
            return View("~/Areas/Administrator/Views/MediaLog/Index.cshtml");
        }

        public ActionResult GetMediaLogs()
        {
            var mediaLogs =
                mediaLogService.GetAll().OrderByDescending(a => a.CreatedDate).Select(
                    mediaLog => new MediaLogDto
                                   {
                                       Id = mediaLog.Id,
                                       CreatedDate = mediaLog.CreatedDate,
                                       Email = mediaLog.Email,
                                       Media = Mapper.Map<Media, MediaDto>(mediaLog.Media)
                                   });
            return Json(mediaLogs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateOrUpdateMediaLog(string models)
        {
            var mediaLogDto =
                JsonConvert.DeserializeObject<List<MediaLogDto>>(models).FirstOrDefault();
            MediaLog mediaLog = Mapper.Map<MediaLogDto, MediaLog>(mediaLogDto);
            mediaLogService.SaveOrUpdate(mediaLog, true);
            return Json(Mapper.Map<MediaLog, MediaLogDto>(mediaLog), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMediaLogs(string ids)
        {
            foreach (MediaLog mediaLog in ids.Split(',').Select(id => mediaLogService.Get(Convert.ToInt32(id))))
            {
                mediaLogService.Delete(mediaLog, true);
            }
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}
