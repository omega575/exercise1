import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { People } from './people/people';
import { Details } from './details/details';
import { PersonDetails } from './person-details/person-details';
import { Duties } from './duties/duties';

const routes: Routes = [
  { path: 'details/:id', component: PersonDetails },
  { path: 'duties', component: Duties },
  { path: 'details', component: Details }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
