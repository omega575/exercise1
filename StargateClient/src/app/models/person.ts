import { AstronautDetail } from "./astronautDetail";
import { AstronautDuty } from "./astronautDuty";

export interface Person {
    personId: number;
    name: string;
    currentRank?: string;
    currentDutyTitle?: string;
    careerStartDate?: Date;
    careerEndDate?: Date
}