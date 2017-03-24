using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LMS1701.USL.UBEAPI.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<LMS1701.USL.UBEAPI.DAL.Batch, Models.Batch>();
                cfg.CreateMap<DAL.User, Models.User>();
                cfg.CreateMap<DAL.UserType, Models.UserType>();
                cfg.CreateMap<DAL.ExamSetting, Models.ExamSetting>();
                cfg.CreateMap<DAL.ExamAssessment, Models.ExamAssessment>();
            });
        }
    }
}