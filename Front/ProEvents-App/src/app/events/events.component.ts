import { Component, OnInit } from '@angular/core';
import { EventService } from '../services/event.service';
import { Event } from '../models/Event';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {
  public events: Event[] = [];
  public filteredEvents: Event[] = [];
  public widthImg: number = 150;
  public marginImg = 2;
  public isToShowImages: boolean = true;

  private _filter: string = '';

  constructor(private _eventService: EventService) { }

  ngOnInit(): void {
    this.getEvents();
  }

  public get filterList(): string {
    return this._filter;
  }

  public set filterList(value: string) {
    this._filter = value;
    this.filteredEvents = this.filterList ? this.filterEvents(this._filter) : this.events;
  }

  private filterEvents(value: string): Event[] {
    value = value.toLowerCase();
    return this.events.filter((event) => event.theme.toLowerCase().indexOf(value) !== -1
      || event.place.toLowerCase().indexOf(value) !== -1);
  }

  private getEvents(): void {
    this._eventService.getEvents().subscribe(
      {
        next: (resp: Event[]) => { this.events = resp, this.filteredEvents = resp },
        error: (error) => { console.log(error) },
        complete: () => { }
      }
    )
  }

  public showImages(): void {
    this.isToShowImages = !this.isToShowImages;
  }

}
