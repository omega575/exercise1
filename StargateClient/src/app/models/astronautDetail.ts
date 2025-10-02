export interface AstronautDetail {
    id: number;
    personId: number;
    currentRank?: string;
    currentDutyTitle?: string;
    careerStartDate: Date;
    careerEndDate?: Date;
}