import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {
  baseUrl: string;
  formulario = new FormGroup({
    usuario: new FormControl(''),
    password: new FormControl(''),
    confirmarPassword: new FormControl('')
  });

  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL_SEGURIDADAPI') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {

  }

  onSubmit() {
    this.http.post<JsonToken>(this.baseUrl + 'api/usuarios/registrar', this.formulario.value)
      .subscribe(result => {
        console.log(result.access_token);
        sessionStorage.setItem('token', result.access_token);
        this.router.navigate(['/home']);
      }, error => console.error(error));
  }

}

interface JsonToken {
  access_token: string
}
