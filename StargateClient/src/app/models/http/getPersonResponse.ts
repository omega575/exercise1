import { Person } from "../person";
import { BaseResponse } from "../baseResponse";

export interface GetPersonResponse extends BaseResponse {
    person: Person;
}