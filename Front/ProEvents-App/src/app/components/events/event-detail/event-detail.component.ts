import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from '@app/services/event.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { Event } from '@models/Event';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.scss']
})
export class EventDetailComponent implements OnInit {
  public eventDetailForm!: FormGroup;
  public event = {} as Event;

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
    private eventService: EventService) { }

  ngOnInit() {
    this.localeService.use('pt-br');
    this.loadEvent();
    this.validation();
  }

  private loadEvent(): void {
    const eventIdParam = this.route.snapshot.paramMap.get('id');

    if (eventIdParam !== null) {
      this.eventService.getEventById(+eventIdParam).subscribe({
        next: (response: Event) => {
          this.event = response,
            this.eventDetailForm.patchValue(this.event)
        },
        error: (err) => { console.log(err) },
        complete: () => { }
      })
    }
  }

  private validation(): void {
    this.eventDetailForm = this.fb.group({
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
      lotes: ['', Validators.required],
      socialMedias: ['', Validators.required],
      eventSpeakers: ['', Validators.required],
    });
  }

  public resetForm(): void {
    this.eventDetailForm.reset();
  }
}
