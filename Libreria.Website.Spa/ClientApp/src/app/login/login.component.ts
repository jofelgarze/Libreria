import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { JsonToken } from '../registro/registro.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formulario = new FormGroup({
    usuario: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL_SEGURIDADAPI') private baseUrl: string) { }

  ngOnInit() {
  }
  
  onSubmit() {
    this.http.post<JsonToken>(this.baseUrl + 'api/usuarios/login', this.formulario.value)
      .subscribe(result => {
        console.log(result.access_token);
        sessionStorage.setItem('token', result.access_token);
        this.router.navigate(['/home']);
      }, error => console.error(error));
  }

}
