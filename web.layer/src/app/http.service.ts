import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, of } from 'rxjs';

@Injectable()
export class HttpService {

    constructor( private httpClient: HttpClient ) {  }

    postData(url: string, data: any, headers: any): Observable<any> {
        const httpOptions = { headers };
        return this.httpClient.post<any>(url, data, httpOptions);
    }

    getData(url: string, headers: any): Observable<any> {
        const httpOptions = { headers };
        return this.httpClient.get(url, httpOptions)
    };

    deleteData(url: string, headers: any): Observable<any> {
        const httpOptions = { headers };
        return this.httpClient.delete<any>(url, httpOptions)
    }


}
