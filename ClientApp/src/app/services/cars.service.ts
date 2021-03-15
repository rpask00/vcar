import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CarsService {

  constructor() { }

  async models(): Promise<any> {
    let models = await fetch("/api/models")
    return models.json();
  }

  async makes(): Promise<any> {
    let makes = await fetch("/api/makes")
    return makes.json();
  }
}
