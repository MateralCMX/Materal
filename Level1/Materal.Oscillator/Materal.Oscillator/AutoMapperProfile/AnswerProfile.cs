using AutoMapper;
using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.DataTransmitModel;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Models.Answer;

namespace Materal.Oscillator.AutoMapperProfile
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<AnswerModel, Answer>()
                .ForMember(m => m.AnswerType, m => m.MapFrom(n => ConvertToAnswerType(n.AnswerData)))
                .ForMember(m => m.AnswerData, m => m.MapFrom(n => ConvertToAnswerData(n.AnswerData)));
            CreateMap<AddAnswerModel, Answer>()
                .ForMember(m => m.AnswerType, m => m.MapFrom(n => ConvertToAnswerType(n.AnswerData)))
                .ForMember(m => m.AnswerData, m => m.MapFrom(n => ConvertToAnswerData(n.AnswerData)));
            CreateMap<EditAnswerModel, Answer>()
                .ForMember(m => m.AnswerType, m => m.MapFrom(n => ConvertToAnswerType(n.AnswerData)))
                .ForMember(m => m.AnswerData, m => m.MapFrom(n => ConvertToAnswerData(n.AnswerData)));
            CreateMap<QueryAnswerManagerModel, QueryAnswerModel>();
            CreateMap<AnswerView, AnswerDTO>()
                .ForMember(m => m.AnswerData, m => m.MapFrom(n => OscillatorConvertHelper.ConvertToInterface<IAnswer>(n.AnswerType, n.AnswerData)));
        }
        private static string ConvertToAnswerType(IAnswer answer) => answer.GetType().Name;
        private static string ConvertToAnswerData(IAnswer answer) => answer.Serialize();
    }
}
