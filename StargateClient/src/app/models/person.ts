import { AstronautDetail } from "./astronautDetail";
import { AstronautDuty } from "./astronautDuty";

export interface Person {
    personId: number;
    name: string;
    detail?: AstronautDetail
    duties?: AstronautDuty[];
}