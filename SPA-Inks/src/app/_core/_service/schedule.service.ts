import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  baseUrl = environment.apiUrlEC;
  scheduleSource = new BehaviorSubject<number>(0);
  currentschedule = this.scheduleSource.asObservable();
  constructor(private http: HttpClient) { }
  getAll() {
    return this.http.get(this.baseUrl + 'schedule/GetAll', {});
  }
  import(file, createdBy) {
    const formData = new FormData();
    formData.append('UploadedFile', file);
    formData.append('CreatedBy', createdBy);
    return this.http.post(this.baseUrl + 'schedule/Import', formData);
  }
  reject(scheduleID, userID) {
    return this.http.post(`${this.baseUrl}schedule/reject/${scheduleID}/${userID}`, {});
  }
  release(scheduleID, userID) {
    return this.http.post(`${this.baseUrl}schedule/release/${scheduleID}/${userID}`, {});
  }
  ProductionDateChange(value , scheduleID) {
    return this.http.post(`${this.baseUrl}schedule/UpdateProductionDate/${value}/${scheduleID}`, {});
  }
  changeDetail(detail) {
    this.scheduleSource.next(detail);
  }

  GetDetailSchedule(scheduleID) {
    return this.http.get(this.baseUrl + `schedule/GetDetailSchedule/${scheduleID}`, {});
  }

  DeleteSchedule(id) {
    return this.http.delete(this.baseUrl + `schedule/Delete/${id}`, {});
  }

  EditSchedule(entity) {
    return this.http.post(this.baseUrl + 'schedule/Editschedule', entity);
  }

  CreateSchedule(entity) {
    return this.http.post(this.baseUrl + 'schedule/CreateSchedule', entity);
  }

  EditPartSchedule(entity) {
    return this.http.post(this.baseUrl + 'schedule/EditPartSchedule', entity);
  }

  addPart(entity) {
    return this.http.post(this.baseUrl + 'part/Create', entity);
  }

  deletePart(id) {
    return this.http.delete(this.baseUrl + `part/Delete/${id}` );
  }

  done(scheduleID) {
    return this.http.post(this.baseUrl + `schedule/done/${scheduleID}`, {});
  }

  approval(scheduleID, userid) {
    return this.http.post(this.baseUrl + `schedule/approve/${scheduleID}/${userid}`, {});
  }

  GetChemicalBySupplier(id) {
    return this.http.get(this.baseUrl + `Chemical/GetChemicalBySupplier/${id}`, {});
  }

}
