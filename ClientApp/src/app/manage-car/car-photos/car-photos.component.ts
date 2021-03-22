import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { PhotosService } from './../../services/photos.service';
import { take } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-car-photos',
  templateUrl: './car-photos.component.html',
  styleUrls: ['./car-photos.component.scss']
})
export class CarPhotosComponent implements OnInit {

  @ViewChild('fileInput', { static: false }) fileInput: ElementRef;
  @Input('id') id: number
  photos$: Observable<any[]>

  constructor(
    private photosSv: PhotosService,
    private toster: ToastrService
  ) { }

  ngOnInit() {
    this.photos$ = this.photosSv.getPhotos(this.id);
    this.photos$.subscribe(console.log)
  }

  uploadFile() {
    let file = this.fileInput.nativeElement.files[0];
    this.photosSv.uploadFile(this.id, file).pipe(take(1))
      .subscribe(photo => {
        this.toster.success("Photo uploaded sucessfully!")
        this.fileInput.nativeElement.value = '';
        this.photos$ = this.photosSv.getPhotos(this.id);
      })
  }

}
