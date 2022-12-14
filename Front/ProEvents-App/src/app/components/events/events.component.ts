import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventService } from '@services/event.service';
import { Event } from '@models/Event';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {
  public readonly title = "Events";

  public events: Event[] = [];
  public filteredEvents: Event[] = [];
  public widthImg: number = 150;
  public marginImg = 2;
  public isToShowImages: boolean = true;

  private _filter: string = '';
  private _modalRef?: BsModalRef;

  constructor(
    private _eventService: EventService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private route: Router
  ) { }

  ngOnInit(): void {

  }

  // public get filterList(): string {
  //   return this._filter;
  // }

  // public set filterList(value: string) {
  //   this._filter = value;
  //   this.filteredEvents = this.filterList ? this.filterEvents(this._filter) : this.events;
  // }

  // private filterEvents(value: string): Event[] {
  //   value = value.toLowerCase();
  //   return this.events.filter((event) => event.theme.toLowerCase().indexOf(value) !== -1
  //     || event.place.toLowerCase().indexOf(value) !== -1);
  // }

  // private getEvents(): void {
  //   this._eventService.getEvents().subscribe(
  //     {
  //       next: (resp: Event[]) => { this.events = resp, this.filteredEvents = resp },
  //       error: (error) => {
  //         this.spinner.hide();
  //         this.toastr.error('Error in loading events.', 'Failed');
  //       },
  //       complete: () => { this.spinner.hide(); }
  //     }
  //   )
  // }

  // public showImages(): void {
  //   this.isToShowImages = !this.isToShowImages;
  // }




  // public openModal(template: TemplateRef<any>): void {
  //   this._modalRef = this.modalService.show(template, { class: 'modal-sm' });
  // }

  // confirmDelete(): void {
  //   this._modalRef?.hide();
  //   this.toastr.success('Event deleted successful.', 'Success');
  // }

  // declineDelete(): void {
  //   this._modalRef?.hide();
  // }


  /**
   * Show Button 'List Events' just when url not contain 'events/list'
   */
  public isShowListBtn(): boolean {
    return !this.route.url.toLowerCase().includes('events/list');
  }
}
