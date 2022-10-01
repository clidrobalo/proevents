import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactsComponent } from './components/contacts/contacts.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { EventDetailComponent } from './components/events/event-detail/event-detail.component';
import { EventListComponent } from './components/events/event-list/event-list.component';
import { EventsComponent } from './components/events/events.component';
import { ProfileComponent } from './components/profile/profile.component';
import { SpeakersComponent } from './components/speakers/speakers.component';

const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  {
    path: 'events', component: EventsComponent,
    children: [
      { path: '', redirectTo: 'list', pathMatch: 'full' },
      { path: 'detail', component: EventDetailComponent },
      { path: 'detail/:id', component: EventDetailComponent },
      { path: 'list', component: EventListComponent }
    ]
  },
  { path: 'speakers', component: SpeakersComponent },
  { path: 'contacts', component: ContactsComponent },
  { path: 'profile', component: ProfileComponent },
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
