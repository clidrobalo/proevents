import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { catchError, Observable, take } from 'rxjs';
import { Event } from '../models/Event';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private _baseURL = environment.apiURL + '/api/event';
  private tokenHeader = new HttpHeaders({ 'Authorization': 'Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIzIiwidW5pcXVlX25hbWUiOiJoaWxsYXJ5LmRpYW5lIiwibmJmIjoxNjY1NjY2MTIzLCJleHAiOjE2NjU3NTI1MjMsImlhdCI6MTY2NTY2NjEyM30.7DoCdeqqqjTzbhLW29Yj-O5Pkf-BW_dg4J57vUp_Pr6ijEVLSiXTxg5SbDor1yDId8nafliLdz1VGlKm6Fimeg' });

  constructor(private _http: HttpClient) { }

  public getEvents(): Observable<Event[]> {
    return this._http.get<Event[]>(`${this._baseURL}/id/${2}`, { headers: this.tokenHeader }).pipe(take(1));
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

  public uploadImage(eventId: number, file: File): Observable<Event> {
    const fileToUpload = file as File;
    const formData = new FormData();
    formData.append('file', fileToUpload);

    return this._http.post<Event>(`${this._baseURL}/upload-image/${eventId}`, formData).pipe(take(1));
  }

  public loadImage(imageURL: string): any {
    return imageURL !== '' ? environment.apiURL + '/resources/images/' + imageURL : 'assets/img/no-photo.png';;
  }
}
