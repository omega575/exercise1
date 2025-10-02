import { Component, OnInit } from '@angular/core';
import { Person } from '../models/person';
import { ActivatedRoute } from '@angular/router';
import { PeopleService } from '../services/people.service';

@Component({
  selector: 'app-person-details',
  standalone: false,
  templateUrl: './person-details.html',
  styleUrl: './person-details.css'
})
export class PersonDetails implements OnInit {
  person: Person | undefined;

  constructor(private route: ActivatedRoute, private peopleService: PeopleService) { }

  ngOnInit(): void {
    this.getPerson();
  }

  getPerson(): void {
    const id = parseInt(this.route.snapshot.paramMap.get('id')!, 10);
    this.peopleService.getPersonById(id).subscribe(response => this.person = response.person);
  }

  save(): void {
    if (this.person) {
      this.peopleService.updatePerson(this.person).subscribe();
    }
  }
}
