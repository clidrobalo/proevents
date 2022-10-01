import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.scss']
})
export class EventDetailComponent implements OnInit {
  eventDetailForm!: FormGroup;

  get form(): any {
    return this.eventDetailForm.controls;
  }

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.validation();
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
