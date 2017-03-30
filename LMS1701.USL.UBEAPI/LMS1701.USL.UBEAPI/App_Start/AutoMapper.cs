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
                cfg.CreateMap<DAL.Batch, Models.Batch>();
                cfg.CreateMap<DAL.User, Models.User>();
                cfg.CreateMap<DAL.UserType, Models.UserType>();
                cfg.CreateMap<DAL.ExamSetting, Models.ExamSetting>();
                cfg.CreateMap<DAL.ExamAssessment, Models.ExamAssessment>();
                cfg.CreateMap<DAL.Roster, Models.Roster>();
                cfg.CreateMap<DAL.StatusType, Models.StatusType>();
                cfg.CreateMap<DAL.QuestionOrder, Models.QuestionOrder>();
                cfg.CreateMap<DAL.QuestionAnswer, Models.QuestionAnswer>();



                cfg.CreateMap<Models.Batch, DAL.Batch>();
                cfg.CreateMap<Models.User, DAL.User>();
                cfg.CreateMap<Models.UserType, DAL.UserType>();
                cfg.CreateMap<Models.ExamSetting, DAL.ExamSetting>();
                cfg.CreateMap<Models.ExamAssessment, DAL.ExamAssessment>();
                cfg.CreateMap<Models.Roster, DAL.Roster>();
                cfg.CreateMap<Models.StatusType, DAL.StatusType>();
                cfg.CreateMap<Models.QuestionOrder, DAL.QuestionOrder>();
            });
        }
    }
}