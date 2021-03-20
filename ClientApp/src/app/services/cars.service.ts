import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Car, Model, Make } from './../interfaces/car';

@Injectable({
  providedIn: 'root'
})
export class CarsService {

  constructor(
    private http: HttpClient
  ) { }

  models(): Observable<Model[]> {
    return this.http.get('/api/models') as Observable<Model[]>;
  }

  makes(): Observable<Make[]> {
    return this.http.get('/api/makes') as Observable<Make[]>;
  }

  features(): Observable<any> {
    return this.http.get('/api/features')
  }

  createCar(car): Observable<any> {
    return this.http.post('/api/cars', car);
  }

  updateCar(car, id): Observable<any> {
    return this.http.put('/api/cars/' + id, car);
  }

  getCar(id: number): Observable<Car> {
    return this.http.get('/api/cars/' + id) as Observable<Car>
  }

  getCars(filter?): Observable<Car[]> {
    let query = []

    if (filter.makeId)
      query.push("MakeId=" + filter.makeId)

    if (filter.modelId)
      query.push("ModelId=" + filter.modelId)

    if (filter.yearmin)
      query.push("yearmin=" + filter.yearmin)

    if (filter.yearmax)
      query.push("yearmax=" + filter.yearmax)

    let filterString = query ? `?` + query.join('&') : ""

    return this.http.get('/api/cars' + filterString) as Observable<Car[]>
  }

  deleteCar(id: number): Observable<Car> {
    return this.http.delete('/api/cars/' + id) as Observable<Car>
  }


}
