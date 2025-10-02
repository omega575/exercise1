import { AstronautDetail } from "../models/astronautDetail";
import { AstronautDuty } from "../models/astronautDuty";
import { Person } from "../models/person";

export const DETAILS: AstronautDetail[] = [
    { id: 1, personId: 1, currentRank: "1LT", currentDutyTitle: "Commander", careerStartDate: new Date() }
];

export const DUTIES: AstronautDuty[] = [
    { id: 1, personId: 1, rank: "1LT", dutyTitle: "Commander", dutyStartDate: new Date() }
];

export const PEOPLE: Person[] = [
    { personId: 1, name: "John Doe" },
    { personId: 2, name: "Jane Doe" }
];