import { IInk } from './../_model/Ink';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class InkService {
  baseUrl = environment.apiUrlEC;
  inkSource = new BehaviorSubject<number>(0);
  currentInk = this.inkSource.asObservable();
  constructor(
    private http: HttpClient
  ) { }

  create(ink: IInk) {
    return this.http.post(this.baseUrl + 'ink/create', ink);
  }
  update(ink: IInk) {
    return this.http.put(this.baseUrl + 'ink/update', ink);
  }
  changeInk(ink) {
    this.inkSource.next(ink);
  }
  getInks() {
    return this.http.get(this.baseUrl + 'Ink/Getall');
  }

  delete(id) {
    return this.http.delete(this.baseUrl + `Ink/delete/${id}`);
  }

  import(file, createdBy) {
    const formData = new FormData();
    formData.append('UploadedFile', file);
    formData.append('CreatedBy', createdBy);
    return this.http.post(this.baseUrl + 'ink/Import', formData);
  }
}
