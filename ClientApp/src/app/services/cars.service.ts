import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {Car, Model, Make, QueryResult} from './../interfaces/car';

@Injectable({
  providedIn: 'root'
})
export class CarsService {

  origin: string

  constructor(
    private http: HttpClient
  ) {
    this.origin = window.location.origin
  }

  models(): Observable<Model[]> {
    return this.http.get(this.origin + '/api/models') as Observable<Model[]>;
  }

  makes(): Observable<Make[]> {
    return this.http.get(this.origin + '/api/makes') as Observable<Make[]>;
  }

  features(): Observable<any> {
    return this.http.get(this.origin + '/api/features')
  }

  createCar(car): Observable<any> {
    console.log(car)
    return this.http.post(this.origin + '/api/cars', car);
  }

  updateCar(car, id): Observable<any> {
    return this.http.put(this.origin + '/api/cars/' + id, car);
  }

  getCar(id: number): Observable<Car> {
    return this.http.get(this.origin + '/api/cars/' + id) as Observable<Car>
  }

  getCars(query?): Observable<QueryResult> {
    return this.http.get(this.origin + '/api/cars?' + this._toQueryString(query)) as Observable<QueryResult>
  }

  private _toQueryString(obj): string {
    let q = []

    for (let key in obj) {
      let value = obj[key]
      if (value !== null && value !== undefined && value !== '' && value !== [])
        q.push(encodeURIComponent(key) + "=" + encodeURIComponent(value))
    }

    return q.join("&")

  }

  deleteCar(id: number): Observable<Car> {
    return this.http.delete(this.origin + '/api/cars/' + id) as Observable<Car>
  }


}
