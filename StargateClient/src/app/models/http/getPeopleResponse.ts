import { Person } from "../person";
import { BaseResponse } from "../baseResponse";

export interface GetPeopleResponse extends BaseResponse {
    people: Person[]
}