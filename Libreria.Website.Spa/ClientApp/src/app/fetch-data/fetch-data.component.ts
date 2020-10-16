import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public autores: Autor[];

  constructor(http: HttpClient, @Inject('BASE_URL_WEBAPI') baseUrl: string) {

    let headersReq = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${sessionStorage.getItem('token')}`
    });

    http.get<Autor[]>(baseUrl + 'api/autores', { headers: headersReq} ).subscribe(result => {
      console.log(result);
      this.autores = result;
    }, error => console.error(error));
  }
}

interface Autor {
  id: number;
  nombre: string;
  fechaRegistro: number;
  libros: [];
  activo: boolean;
}
