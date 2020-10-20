import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  private headersReq: HttpHeaders;
  
  public autores: Autor[];  
  public formularioAutor = new FormGroup({
    id: new FormControl(''),
    nombre: new FormControl(''),
    fechaRegistro: new FormControl(''),
    libros: new FormControl(''),
    activo: new FormControl('')
  });

  constructor(private http: HttpClient, @Inject('BASE_URL_WEBAPI') private baseUrl: string) {}

  ngOnInit(): void {
    this.headersReq = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${sessionStorage.getItem('token')}`
    });

    this.consultarAutores();
  }

  consultarAutores() {
    this.http.get<Autor[]>(this.baseUrl + 'api/autores', { headers: this.headersReq })
      .subscribe(result => {
        console.log(result);
        this.autores = result;
      }, error => console.error(error));
  }

  crearAutor() {
    debugger;
    var body = this.formularioAutor.value;
    delete body.id;

    this.http.post<Autor>(this.baseUrl + 'api/autores',
      this.formularioAutor.value,
      { headers: this.headersReq }
    ).subscribe(result => {
      console.log('Se ha eliminado: ' + result);
      this.formularioAutor.reset();
      this.consultarAutores();
    }, error => console.error(error));

  }

  acualizarAutor() {

    var body = this.formularioAutor.value;
    body.id = Number(body.id);    

    this.http.put<any>(this.baseUrl + 'api/autores/' + this.formularioAutor.controls['id'].value,
      body,
      { headers: this.headersReq }
    ).subscribe(result => {
      console.log('Se ha eliminado: ' + JSON.stringify(this.formularioAutor.value));
      this.formularioAutor.reset();
      this.consultarAutores();
    }, error => console.error(error));

  }

  eliminarAutor() {
    this.http.delete<Autor>(this.baseUrl + 'api/autores/' + this.formularioAutor.controls['id'].value,
      { headers: this.headersReq }
    ).subscribe(result => {
      console.log('Se ha eliminado: ' + JSON.stringify(result));
      this.formularioAutor.reset();
      this.consultarAutores();
    }, error => console.error(error));

  }

  seleccionarAutor(autor: Autor) {
    this.formularioAutor.reset();
    this.formularioAutor.setValue(autor);
  }

  onSubmit() {
    if (this.formularioAutor.controls['id'].value) {
      this.acualizarAutor();
    } else {
      this.crearAutor();
    }
  }

}

interface Autor {
  id: number;
  nombre: string;
  fechaRegistro: Date;
  libros: [];
  activo: boolean;
}
