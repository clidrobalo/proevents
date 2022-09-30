import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {
  public events: any = [];
  public filteredEvents: any = [];
  widthImg = 150;
  marginImg = 2;
  isToShowImages: boolean = true;

  private _filter: string = '';

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEvent();
  }

  public get filterList() {
    return this._filter;
  }

  public set filterList(value: string) {
    this._filter = value;
    this.filteredEvents = this.filterList ? this.filterEvents(this._filter) : this.events;
  }

  private filterEvents(value: string): any {
    value = value.toLowerCase();
    return this.events.filter((event: any) => event.theme.toLowerCase().indexOf(value) !== -1
    || event.place.toLowerCase().indexOf(value) !== -1);
  }

  private getEvent(): void {
    this.http.get('https://localhost:5001/api/event').subscribe(
      response => {this.events = response, this.filteredEvents = response},
      error => console.log(error)
    )
  }

  public showImages(): void{
    this.isToShowImages = !this.isToShowImages;
  }

}
