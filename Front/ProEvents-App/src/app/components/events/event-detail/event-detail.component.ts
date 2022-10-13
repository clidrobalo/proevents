import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from '@app/services/event.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { Event } from '@models/Event';
import { Lote } from '@models/Lote';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { LoteService } from '@app/services/lote.service';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { environment } from '@environments/environment';

@Component({
    selector: 'app-event-detail',
    templateUrl: './event-detail.component.html',
    styleUrls: ['./event-detail.component.scss']
})
export class EventDetailComponent implements OnInit {
    public eventDetailForm!: FormGroup;
    public event = {} as Event;
    public isToUpdate: boolean = false;
    public imageURL: string = '/assets/img/cloud-computing.png';
    public file: any;

    private _modalRef?: BsModalRef;

    public loteAtual: {
        id: number,
        index: number,
        name: string
    } = { id: 0, index: 0, name: '' };

    get form(): any {
        return this.eventDetailForm.controls;
    }

    get lotes(): FormArray {
        return this.form['lotes'] as FormArray;
    }

    get bsConfig(): any {
        return {
            adaptivePosition: true,
            dateInputFormat: 'DD-MM-YYYY hh:mm a',
            containerClass: 'theme-default',
            showWeekNumbers: false
        }
    }

    constructor(private fb: FormBuilder,
        private localeService: BsLocaleService,
        private activetedRoute: ActivatedRoute,
        private router: Router,
        private eventService: EventService,
        private loteService: LoteService,
        private spinnerService: NgxSpinnerService,
        private toastService: ToastrService,
        private modalService: BsModalService) { }

    ngOnInit() {
        this.localeService.use('pt-br');
        this.loadEvent();
        this.validation();
    }

    public resetForm(): void {
        this.eventDetailForm.reset();
    }

    public saveEvent(): void {
        this.spinnerService.show();

        this.event = { ... this.eventDetailForm.value };

        this.eventService.addEvent(this.event).subscribe({
            next: (resp: Event) => {
                this.toastService.success("Event saved successful.", "Success");
                // To enable Lotes form
                this.router.navigate([`events/detail/${resp.id}`]);
            },
            error: (err: any) => {
                console.log(err);
                this.spinnerService.hide();
                this.toastService.error("Error in Save Event.", "Error")
            },
            complete: () => { this.spinnerService.hide(); }
        })
    }

    public updateEvent(): void {
        this.spinnerService.show();

        this.event = { ... this.eventDetailForm.value };

        this.eventService.updateEvent(this.event).subscribe({
            next: (resp: Event) => { this.toastService.success("Event updated successful.", "Success"); },
            error: (err: any) => {
                console.log(err);
                this.spinnerService.hide();
                this.toastService.error("Error in Update Event.", "Error")
            },
            complete: () => { this.spinnerService.hide(); }
        })
    }

    public saveLote(): void {
        this.spinnerService.show();

        var lotes: Lote[] = this.form['lotes'].value;

        this.loteService.saveLotes(this.event.id, lotes).subscribe({
            next: (resp: Lote[]) => { this.toastService.success("Lotes saved successful.", "Success"); },
            error: (err) => {
                console.log(err);
                this.spinnerService.hide();
                this.toastService.error("Error in Save Lotes.", "Error")
            },
            complete: () => { this.spinnerService.hide(); }
        })
    }

    public addLote(): void {
        this.lotes.push(this.createLote({ id: 0 } as Lote));
    }

    public deleteLote(index: number): void {
        this.loteAtual = {
            id: this.lotes.get(index + '.id')?.value,
            index: index,
            name: this.lotes.get(index + '.name')?.value
        }
    }

    public confirmLoteDelete(): void {
        this._modalRef?.hide();
        this.spinnerService.show();

        this.loteService.deleteLoteById(this.loteAtual.id).subscribe({
            next: (resp: string) => { this.toastService.success(resp, 'Success'); this.lotes.removeAt(this.loteAtual.index); },
            error: (error: any) => {
                this.spinnerService.hide();
                console.log(error);
                this.toastService.error("Error in deleting Lote. Please Contact Support", 'Failed');
            },
            complete: () => { this.spinnerService.hide(); }
        });
    }

    public declineLoteDelete(): void {
        this._modalRef?.hide();
    }

    // MODAL - START
    public openModal(template: TemplateRef<any>, index: number): void {
        var mo = new ModalOptions();
        mo.class = 'modal-md';
        mo.ignoreBackdropClick = true;

        this.loteAtual = {
            id: this.lotes.get(index + '.id')?.value,
            index: index,
            name: this.lotes.get(index + '.name')?.value
        }

        this._modalRef = this.modalService.show(template, mo);
    }

    public onFileChange(event: any): void {
        const reader = new FileReader();

        reader.onload = (event: any) => this.imageURL = event.target.result;

        this.file = event.target.files;
        reader.readAsDataURL(this.file[0]);

        this.uploadImage();
    }

    private uploadImage(): void {
        this.spinnerService.show();
        this.eventService.uploadImage(this.event.id, this.file[0]).subscribe({
            next: (event: Event) => {
                this.loadEvent();
                this.toastService.success('Image uploaded successful.', 'Success')
            },
            error: (error: any) => {
                this.toastService.error('Error in Image uploaded.', 'Failed')
            },
            complete: () => {
                this.spinnerService.hide();
            }
        })
    }

    private createLote(lote: Lote): FormGroup {
        return this.fb.group({
            id: [lote.id],
            name: [lote.name, Validators.required],
            price: [lote.price, Validators.required],
            startDate: [lote.startDate, Validators.required],
            endDate: [lote.endDate, Validators.required],
            quantity: [lote.quantity, Validators.required],
        })
    }

    private loadEvent(): void {
        const eventIdParam = this.activetedRoute.snapshot.paramMap.get('id');

        if (eventIdParam !== null) {
            this.isToUpdate = true;
            // show spinner
            this.spinnerService.show();

            this.eventService.getEventById(+eventIdParam).subscribe({
                next: (response: Event) => {
                    this.event = response,
                        this.eventDetailForm.patchValue(this.event);

                    if (this.event.imageUrl !== '') {
                        this.imageURL = `${environment.apiURL}/resources/images/${this.event.imageUrl}`;
                    }

                    this.event.lotes.forEach(lote => {
                        this.lotes.push(this.createLote(lote));
                    });

                    this.toastService.success("Event detail loaded successful.", "Success");
                },
                error: (err) => {
                    console.log(err);
                    this.toastService.error("Error trying load the Event Detail.", "Error");
                    // Hide spinner
                    this.spinnerService.hide();
                },
                complete: () => {
                    // Hide spinner
                    this.spinnerService.hide();
                },
            })
        } else {
            this.isToUpdate = false;
        }
    }

    private validation(): void {
        this.eventDetailForm = this.fb.group({
            id: [0],
            theme: ['', [
                Validators.required,
                Validators.minLength(4),
                Validators.maxLength(50)
            ]],
            place: ['', Validators.required],
            eventDate: ['', Validators.required],
            numberOfPerson: ['', [
                Validators.required,
                Validators.max(120000)
            ]],
            phone: ['', Validators.required],
            email: ['', [
                Validators.required,
                Validators.email]],
            imageUrl: ['', Validators.required],
            lotes: this.fb.array([]),
            userId: 6
        });
    }
}
