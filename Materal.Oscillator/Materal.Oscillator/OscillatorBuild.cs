using AutoMapper;
using Materal.Common;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Models.Answer;
using Materal.Oscillator.Abstractions.Models.Plan;
using Materal.Oscillator.Abstractions.Models.Schedule;
using Materal.Oscillator.Abstractions.Models.Work;
using Materal.Oscillator.Abstractions.Models.WorkEvent;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.Models;
using Materal.TTA.Common;

namespace Materal.Oscillator
{
    public class OscillatorBuild : IOscillatorBuild
    {
        private readonly ScheduleModel _schedule;
        private readonly List<AnswerModel> _answers = new();
        private readonly List<WorkEventModel> _workEvents = new();
        private readonly List<PlanModel> _plans = new();
        private readonly List<ScheduleWorkView> _scheduleWorks = new();
        private readonly ScheduleOperationModel _oscillatorManager;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        private readonly IOscillatorUnitOfWork _unitOfWork;
        public OscillatorBuild(ScheduleModel schedule, ScheduleOperationModel scheduleIperationModel)
        {
            _schedule = schedule;
            _oscillatorManager = scheduleIperationModel;
            _scheduleRepository = MateralServices.GetService<IScheduleRepository>();
            _mapper = MateralServices.GetService<IMapper>();
            _unitOfWork = MateralServices.GetService<IOscillatorUnitOfWork>();
        }
        public IOscillatorBuild AddAnswer(AnswerModel model)
        {
            _answers.Add(model);
            return this;
        }
        public IOscillatorBuild AddWorkEvent(WorkEventModel model)
        {
            _workEvents.Add(model);
            return this;
        }
        public IOscillatorBuild AddPlan(PlanModel model)
        {
            _plans.Add(model);
            return this;
        }
        public IOscillatorBuild AddWork(WorkModel model)
        {
            ScheduleWorkView scheduleWork = _mapper.Map<ScheduleWorkView>(model);
            scheduleWork.OrderIndex = _scheduleWorks.Count + 1;
            _scheduleWorks.Add(scheduleWork);
            return this;
        }
        public IOscillatorBuild AddWork(Guid workID)
        {
            ScheduleWorkView scheduleWork = new()
            {
                WorkID = workID,
                OrderIndex = _scheduleWorks.Count + 1
            };
            _scheduleWorks.Add(scheduleWork);
            return this;
        }
        public async Task<Guid> BuildAsync()
        {
            Schedule schedule = _mapper.Map<Schedule>(_schedule);
            _oscillatorManager.SetTerritoryProperties(schedule);
            _unitOfWork.RegisterAdd(schedule);
            Plan[] plans = _mapper.Map<Plan[]>(_plans);
            foreach (Plan plan in plans)
            {
                _unitOfWork.RegisterAdd(plan);
            }
            WorkEvent[] workEvents = _mapper.Map<WorkEvent[]>(_workEvents);
            foreach (WorkEvent workEvent in workEvents)
            {
                _unitOfWork.RegisterAdd(workEvent);
            }
            Answer[] answers = _mapper.Map<Answer[]>(_answers);
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].OrderIndex = i + 1;
                _unitOfWork.RegisterAdd(answers[i]);
            }
            await _unitOfWork.CommitAsync();
            return schedule.ID;
        }
    }
}
