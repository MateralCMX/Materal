using Materal.BusinessFlow.Abstractions.Expressions;

namespace Materal.BusinessFlow.TestConsole
{
    public class NodeRunConditionTestHandler : BaseTestHandler
    {
        public override void Excute()
        {
            //F:字段 C:常量
            Dictionary<string, object?> fieldDatas = new()
            {
                ["Name"] = "陈明旭",
                ["Age"] = 29,
                ["Score"] = 100
            };
            //(F.Age>25)&&(F.Score>60)&&F.Name=="陈明旭"
            const string expression = "(({F|Age}[GreaterThan]{C|25|Number})[And]({F|Score}[GreaterThan]{C|60|Number}))[And]({F|Name}[Equal]{C|陈明旭|String})";
            //F.Data == 1
            //const string expression = "{F|Data}[Equal]{C|1|Number}";
            //1==1
            //const string expression = "{C|1|Number}[Equal]{C|1|Number}";
            ConditionExpression conditionExpression = ConditionExpression.Parse(expression);
            string json = conditionExpression.ToJson();
            var a = conditionExpression.GetValue(fieldDatas);
        }
    }
}
