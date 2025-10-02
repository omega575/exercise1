import { catchError, Observable, tap } from "rxjs";
import { PEOPLE } from "../data/mock";
import { Person } from "../models/person";
import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { GetPeopleResponse } from "../models/http/getPeopleResponse";
import { GetPersonResponse } from "../models/http/getPersonResponse";

@Injectable({ providedIn: 'root' })
export class PeopleService {
    private apiUrl = 'https://localhost:7204/person';

    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }

    constructor(private http: HttpClient) { }

    getPeople(): Observable<GetPeopleResponse> {
        return this.http.get<GetPeopleResponse>(this.apiUrl)
            .pipe(
                tap(response => console.log(response)))
    }

    getPersonByName(name: string): Observable<GetPersonResponse> {
        const url = `${this.apiUrl}/${name}`;
        return this.http.get<GetPersonResponse>(url).pipe(tap(response => {
            console.log(response);
        }));
    }

    getPersonById(id: number): Observable<GetPersonResponse> {
        const url = `${this.apiUrl}/get?id=${id}`;
        return this.http.get<GetPersonResponse>(url).pipe(tap(response => {
            console.log(response);
        }));
    }
}