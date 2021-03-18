import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, pipe } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarsService {

  constructor(
    private http: HttpClient
  ) { }

  models(): Observable<any> {
    return this.http.get('/api/models');
  }

  makes(): Observable<any> {
    return this.http.get('/api/makes');
  }

  features(): Observable<any> {
    return this.http.get('/api/features')
  }

  saveCar(car) {
    return this.http.post('/api/cars', car);
  }


}
