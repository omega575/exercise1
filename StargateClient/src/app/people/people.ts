import { Component } from '@angular/core';
import { PEOPLE } from '../data/mock';
import { Person } from '../models/person';
import { PeopleService } from '../services/people.service';
import { OnInit } from '@angular/core';
import { GetPersonResponse } from '../models/http/getPersonResponse';

@Component({
  selector: 'app-people',
  standalone: false,
  templateUrl: './people.html',
  styleUrl: './people.css',
})
export class People {
  people: Person[] = [];

  constructor(private peopleService: PeopleService) { }


  ngOnInit(): void {
    this.getPeople();
  }

  getPeople(): void {
    this.peopleService.getPeople().subscribe(response => this.people = response.people);
  }

  something(person: Person) {
    console.log(person);
  }


}
