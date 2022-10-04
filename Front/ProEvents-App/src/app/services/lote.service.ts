import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Lote } from '../models/Lote';

@Injectable({
  providedIn: 'root'
})
export class LoteService {
  private _baseURL = 'https://localhost:5001/api/lote';

  constructor(private _http: HttpClient) { }

  public getLotes(): Observable<Lote[]> {
    return this._http.get<Lote[]>(this._baseURL).pipe(take(1));
  }

  public getLoteById(id: number): Observable<Lote> {
    return this._http.get<Lote>(`${this._baseURL}/id/${id}`).pipe(take(1));
  }

  public getLoteByEventId(eventId: number): Observable<Lote[]> {
    const options = { params: new HttpParams().set('event-id', eventId) };
    return this._http.get<Lote[]>(`${this._baseURL}/eventid/${eventId}`).pipe(take(1));
  }

  public saveLotes(eventId: number, lotes: Lote[]): Observable<Lote[]> {
    const options = { params: new HttpParams().set('eventId', eventId) };
    return this._http.post<Lote[]>(this._baseURL, lotes, options).pipe(take(1));
  }

  public deleteLoteById(id: number) {
    return this._http.delete(`${this._baseURL}/${id}`, { responseType: 'text' }).pipe(take(1));
  }
}
