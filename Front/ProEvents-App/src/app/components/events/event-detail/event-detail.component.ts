import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from '@app/services/event.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { Event } from '@models/Event';
import { NgxSpinnerService } from 'ngx-spinner';
import { Toast, ToastrService } from 'ngx-toastr';
import { timeStamp } from 'console';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.scss']
})
export class EventDetailComponent implements OnInit {
  public eventDetailForm!: FormGroup;
  public event = {} as Event;
  public isToUpdate: boolean = false;

  get form(): any {
    return this.eventDetailForm.controls;
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
    private route: ActivatedRoute,
    private eventService: EventService,
    private spinnerService: NgxSpinnerService,
    private toastService: ToastrService) { }

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
      next: (resp: Event) => { this.toastService.success("Event saved successful.", "Success"); },
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

  private loadEvent(): void {
    const eventIdParam = this.route.snapshot.paramMap.get('id');

    if (eventIdParam !== null) {
      this.isToUpdate = true;
      // show spinner
      this.spinnerService.show();

      this.eventService.getEventById(+eventIdParam).subscribe({
        next: (response: Event) => {
          this.event = response,
            this.eventDetailForm.patchValue(this.event);
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
        }
      })
    } else {
      this.isToUpdate = false;
    }
  }

  private validation(): void {
    this.eventDetailForm = this.fb.group({
      id: [''],
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
      imageUrl: ['', Validators.required]
    });
  }

}
