import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Event } from '../models/Event';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private _baseURL = 'https://localhost:5001/api/event';

  constructor(private _http: HttpClient) { }

  public getEvents(): Observable<Event[]> {
    return this._http.get<Event[]>(this._baseURL);
  }

  public getEventById(id: number): Observable<Event> {
    return this._http.get<Event>(`${this._baseURL}/id/${id}`);
  }

  public getEventsByTheme(theme: string): Observable<Event[]> {
    return this._http.get<Event[]>(`${this._baseURL}/theme/${theme}`);
  }

  public addEvent(event: Event): Observable<Event> {
    return this._http.post<Event>(this._baseURL, event);
  }

  public updateEvent(event: Event): Observable<Event> {
    return this._http.put<Event>(this._baseURL, event);
  }

  public deleteEvent(id: number): Observable<string> {
    return this._http.delete<string>(`${this._baseURL}/${id}`);
  }
}
