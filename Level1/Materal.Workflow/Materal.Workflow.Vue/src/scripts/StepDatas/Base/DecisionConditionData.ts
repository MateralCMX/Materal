import { ConditionEnum } from "./ConditionEnum";
import { ValueSourceEnum } from "./ValueSourceEnum";
import { ComparisonTypeEnum } from "./ComparisonTypeEnum";

export class DecisionConditionData {
    public LeftValue: string | number = "";
    public LeftValueSource: ValueSourceEnum = ValueSourceEnum.Constant;
    public ComparisonType: ComparisonTypeEnum = ComparisonTypeEnum.Equal;
    public RightValue: string | number = "";
    public RightValueSource: ValueSourceEnum = ValueSourceEnum.Constant;
    public Condition: ConditionEnum = ConditionEnum.And;
}