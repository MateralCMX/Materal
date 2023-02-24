using AutoMapper;
using Materal.Abstractions;
using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.QuartZExtend;
using Materal.Oscillator.Abstractions.Repositories;

namespace Materal.Oscillator.Answers
{
    /// <summary>
    /// 禁止调度器
    /// </summary>
    public class DisableScheduleAnswer : AnswerBase, IAnswer
    {
        private IMapper? _mapper;
        private IOscillatorUnitOfWork? _unitOfWork;
        private IScheduleRepository? _scheduleRepository;
        private OscillatorService? _oscillatorService;

        public override async Task InitAsync()
        {
            _mapper = MateralServices.GetService<IMapper>();
            _unitOfWork = MateralServices.GetService<IOscillatorUnitOfWork>();
            _scheduleRepository = MateralServices.GetService<IScheduleRepository>();
            _oscillatorService = MateralServices.GetService<OscillatorService>();
            await base.InitAsync();
        }

        public override async Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWorkView scheduleWork, Answer answer, IOscillatorJob job)
        {
            if (_oscillatorService == null) return true;
            await _oscillatorService.StopAsync(null, schedule.ID);
            if (_scheduleRepository == null) return true;
            schedule.Enable = false;
            Schedule? scheduleFromDB = await _scheduleRepository.FirstOrDefaultAsync(schedule.ID);
            if (scheduleFromDB == null || _unitOfWork == null || _mapper == null) return true;
            _mapper.Map(schedule, scheduleFromDB);
            _unitOfWork.RegisterEdit(scheduleFromDB);
            await _unitOfWork.CommitAsync();
            return false;
        }
    }
}
