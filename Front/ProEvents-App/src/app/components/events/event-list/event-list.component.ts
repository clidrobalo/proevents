import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventService } from '@services/event.service';
import { Event } from '@models/Event';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { environment } from '@environments/environment';
import { Pagination, PaginationResult } from '@app/models/Pagination';
import { debounceTime, Subject } from 'rxjs';

@Component({
    selector: 'app-event-list',
    templateUrl: './event-list.component.html',
    styleUrls: ['./event-list.component.scss']
})
export class EventListComponent implements OnInit {

    public readonly title = "Events";

    public events: Event[] = [];
    public filteredEvents: Event[] = [];
    public widthImg: number = 150;
    public marginImg = 2;
    public isToShowImages: boolean = true;
    public eventTheme = '';
    public pagination = {} as Pagination;
    public termoBuscaChanged: Subject<string> = new Subject<string>();


    private _filter: string = '';
    private _modalRef?: BsModalRef;
    private _eventId!: number;


    constructor(
        private _eventService: EventService,
        private modalService: BsModalService,
        private toastr: ToastrService,
        private spinner: NgxSpinnerService,
        private route: Router
    ) { }

    ngOnInit(): void {
        this.pagination = {
            currentPage: 1,
            itemsPerPage: 3,
            totalItems: 1,
        } as Pagination;

        this.getEvents();
    }

    public get filterList(): string {
        return this._filter;
    }

    public set filterList(value: string) {
        if (this.termoBuscaChanged.observers.length === 0) {
            this.termoBuscaChanged
                .pipe(debounceTime(1000))
                .subscribe((filtrarPor) => {
                    this.spinner.show();
                    this._eventService
                        .getEvents(
                            this.pagination.currentPage,
                            this.pagination.itemsPerPage,
                            filtrarPor
                        )
                        .subscribe(
                            (paginatedResult: PaginationResult<Event[]>) => {
                                this.events = paginatedResult.result;
                                this.filteredEvents = paginatedResult.result;
                                this.pagination = paginatedResult.pagination;
                            },
                            (error: any) => {
                                this.spinner.hide();
                                this.toastr.error('Erro ao Carregar os Eventos', 'Erro!');
                            }
                        )
                        .add(() => this.spinner.hide());
                });
        }
        this.termoBuscaChanged.next(value);
    }

    public showImages(): void {
        this.isToShowImages = !this.isToShowImages;
    }

    // MODAL - START
    public openModal(event: any, template: TemplateRef<any>, eventTheme: string, eventId: number): void {
        // stopPropagation to not open Event Detail page
        event.stopPropagation();

        var mo = new ModalOptions();
        mo.class = 'modal-md';
        mo.ignoreBackdropClick = true;

        this.eventTheme = eventTheme;
        this._eventId = eventId;

        this._modalRef = this.modalService.show(template, mo);
    }

    public confirmDelete(): void {
        this._modalRef?.hide();
        this.spinner.show();

        this._eventService.deleteEventById(this._eventId).subscribe({
            next: (resp: string) => { this.toastr.success(resp, 'Success'); },
            error: (error: any) => {
                this.spinner.hide();
                console.log(error);
                this.toastr.error("Error in deleting Event. Please Contact Support", 'Failed');
            },
            complete: () => { this.spinner.hide(); this.getEvents() }
        });
    }

    public declineDelete(): void {
        this._modalRef?.hide();
    }
    // MODAL - END

    public showDetail(id: number): void {
        this.route.navigate([`events/detail/${id}`]);
    }

    public loadImage(imageURL: string) {
        return this._eventService.loadImage(imageURL);
    }

    public pageChanged(event: any): void {
        this.pagination.currentPage = event.page;
        this.getEvents();
    }

    private filterEvents(value: string): Event[] {
        value = value.toLowerCase();
        return this.events.filter((event) => event.theme.toLowerCase().indexOf(value) !== -1
            || event.place.toLowerCase().indexOf(value) !== -1);
    }

    private getEvents(): void {
        this.spinner.show();

        this._eventService.getEvents(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe(
            {
                next: (resp: PaginationResult<Event[]>) => {
                    this.events = resp.result;
                    this.filteredEvents = resp.result;
                    this.pagination = resp.pagination;
                },
                error: (error) => {
                    this.spinner.hide();
                    this.toastr.error('Error in loading events.', 'Failed');
                },
                complete: () => { this.spinner.hide(); }
            }
        )
    }
}
