//using Materal.Abstractions;
//using Materal.Oscillator.Abstractions.Answers;
//using Materal.Oscillator.Abstractions.Domain;
//using Materal.Oscillator.Abstractions.QuartZExtend;
//using Materal.Oscillator.Abstractions.Repositories;

//namespace Materal.Oscillator.Answers
//{
//    /// <summary>
//    /// 删除调度器
//    /// </summary>
//    public class DeleteScheduleAnswer : AnswerBase, IAnswer
//    {
//        private IOscillatorUnitOfWork? _unitOfWork;
//        private IScheduleRepository? _scheduleRepository;
//        private OscillatorService? _oscillatorService;

//        public override async Task InitAsync()
//        {
//            _unitOfWork = MateralServices.GetService<IOscillatorUnitOfWork>();
//            _scheduleRepository = _unitOfWork.GetRepository<IScheduleRepository>();
//            _oscillatorService = MateralServices.GetService<OscillatorService>();
//            await base.InitAsync();
//        }

//        public override async Task<bool> ExcuteAsync(string eventValue, Schedule schedule, ScheduleWork scheduleWork, Answer answer, IOscillatorJob job)
//        {
//            if (_oscillatorService == null) return true;
//            await _oscillatorService.StopAsync(null, schedule.ID);
//            if (_scheduleRepository == null) return true;
//            Schedule? scheduleFromDB = await _scheduleRepository.FirstOrDefaultAsync(schedule.ID);
//            if(scheduleFromDB == null || _unitOfWork == null) return true;
//            _unitOfWork.RegisterDelete(scheduleFromDB);
//            await _unitOfWork.CommitAsync();
//            return false;
//        }
//    }
//}
