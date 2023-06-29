import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class PhotosService {

  constructor(
    private http: HttpClient
  ) { }

  uploadFile(carId, photo): Observable<any> {
    let formData = new FormData()
    formData.append('file', photo);

    return this.http.post(`api/cars/${carId}/photos`, formData, {
      reportProgress: true,
      observe: 'events'
    });

  }

  getPhotos(carId): Observable<any> {
    return this.http.get(`api/cars/${carId}/photos`);
  }
}

