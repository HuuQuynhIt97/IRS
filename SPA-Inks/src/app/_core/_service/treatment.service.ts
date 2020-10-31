import { ITreatmentWay } from './../_model/TreatmentWay';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { ISupplier } from '../_model/Supplier';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    // 'Authorization': 'Bearer ' + localStorage.getItem('token'),
  }),
};
@Injectable({
  providedIn: 'root'
})
export class TreatmentService {
  baseUrl = environment.apiUrlEC;
  constructor(
    private http: HttpClient
  ) { }

  getAll() {
    return this.http.get(this.baseUrl + 'Process/GetAllTreatmentWay', {});
  }
  getAllProcess() {
    return this.http.get(this.baseUrl + 'Process/GetAll', {});
  }
  create(treatmentWay: ITreatmentWay) {
    return this.http.post(this.baseUrl + 'Process/CreateTreatmentWay', treatmentWay);
  }
  update(treatmentWay: ITreatmentWay) {
    return this.http.put(this.baseUrl + 'Process/UpdateTreatmentWay', treatmentWay);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + 'Process/DeleteTreatmentWay/' + id);
  }
}
