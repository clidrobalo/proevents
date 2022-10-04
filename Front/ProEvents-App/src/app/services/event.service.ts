import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Event } from '../models/Event';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private _baseURL = 'https://localhost:5001/api/event';

  constructor(private _http: HttpClient) { }

  public getEvents(): Observable<Event[]> {
    return this._http.get<Event[]>(this._baseURL).pipe(take(1));
  }

  public getEventById(id: number): Observable<Event> {
    return this._http.get<Event>(`${this._baseURL}/id/${id}`).pipe(take(1));
  }

  public getEventsByTheme(theme: string): Observable<Event[]> {
    return this._http.get<Event[]>(`${this._baseURL}/theme/${theme}`).pipe(take(1));
  }

  public addEvent(event: Event): Observable<Event> {
    return this._http.post<Event>(this._baseURL, event).pipe(take(1));
  }

  public updateEvent(event: Event): Observable<Event> {
    return this._http.put<Event>(this._baseURL, event).pipe(take(1));
  }

  public deleteEventById(id: number) {
    return this._http.delete(`${this._baseURL}/${id}`, { responseType: 'text' }).pipe(take(1));
  }
}
