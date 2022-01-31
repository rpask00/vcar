import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { PhotosService } from './../../services/photos.service';
import { take } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { HttpEvent, HttpEventType, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-car-photos',
  templateUrl: './car-photos.component.html',
  styleUrls: ['./car-photos.component.scss']
})
export class CarPhotosComponent implements OnInit {

  @ViewChild('fileInput') fileInput: ElementRef;
  @Input('id') id: number

  dowloadProgress: string
  photos$: Observable<any[]>

  constructor(
    private photosSv: PhotosService,
    private toster: ToastrService,
    private http: HttpClient
  ) { }

  ngOnInit() {

    this.photos$ = this.photosSv.getPhotos(this.id);
  }

  uploadFile() {
    let file = this.fileInput.nativeElement.files[0];
    if (!file) return

    this.photosSv.uploadFile(this.id, file)
      .subscribe((event: HttpEvent<any>) => {
        switch (event.type) {
          case HttpEventType.UploadProgress:
            this.dowloadProgress = Math.ceil(event.loaded / event.total * 100) + "%"
            break;

          case HttpEventType.Response:
            this.toster.success("Photo uploaded sucessfully!")
            this.dowloadProgress = null;
            this.fileInput.nativeElement.value = '';
            this.photos$ = this.photosSv.getPhotos(this.id);
            break;
        }
      })
  }

}
