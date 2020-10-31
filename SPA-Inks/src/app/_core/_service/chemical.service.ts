import { IChemical } from './../_model/Chemical';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChemicalService {

  baseUrl = environment.apiUrlEC;
  chemicalSource = new BehaviorSubject<number>(0);
  currentChemical = this.chemicalSource.asObservable();
  constructor(
    private http: HttpClient
  ) { }

  create(chemical: IChemical) {
    return this.http.post(this.baseUrl + 'Chemical/create', chemical);
  }
  update(chemical: IChemical) {
    return this.http.put(this.baseUrl + 'Chemical/update', chemical);
  }
  changeChemical(chemical) {
    this.chemicalSource.next(chemical);
  }
  getChemicals() {
    return this.http.get(this.baseUrl + 'Chemical/Getall');
  }
  delete(id) {
    return this.http.delete(this.baseUrl + `Chemical/Delete/${id}`);
  }
  import(file, createdBy) {
    const formData = new FormData();
    formData.append('UploadedFile', file);
    formData.append('CreatedBy', createdBy);
    return this.http.post(this.baseUrl + 'Chemical/Import', formData);
  }
}
