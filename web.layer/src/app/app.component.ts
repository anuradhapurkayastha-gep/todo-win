import { HttpClient } from '@angular/common/http';
import { ChangeDetectionStrategy, Component,OnInit,OnDestroy,ChangeDetectorRef,TemplateRef, ViewContainerRef,ViewChild} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection:ChangeDetectionStrategy.OnPush,
})

export class AppComponent implements OnInit, OnDestroy {

  @ViewChild('selectContainer', { read: ViewContainerRef, static: true }) selectContainer: ViewContainerRef;
  @ViewChild("home",{static: true})  home:TemplateRef<any>;
  @ViewChild("trending",{static: true})  trending:TemplateRef<any>;
  @ViewChild("history",{static: true})  history:TemplateRef<any>;
  public columnData: any = [];

  constructor(public cdRef: ChangeDetectorRef, public http: HttpClient ) {}

  ngOnInit() { 
      this.gridData();
  }

  gridData(){
    this.http.get('https://localhost:44314/report').subscribe(data => {
      this.columnData = data;
      this.loadPage();
    },
    error => {
        console.log('Log the error here: ', error);
    });
  }

  onTabClick($event) {
     this.loadPage($event);
  }

  loadPage (selected =null) {
    if(!selected || (selected == 1)) {
      this.selectContainer.length >0  && this.selectContainer.clear();
      this.selectContainer.createEmbeddedView(this.home);
    }
    if (selected == 2) {
      this.selectContainer.length >0  && this.selectContainer.clear();
      this.selectContainer.createEmbeddedView(this.trending);
    }
    if (selected == 3) {
      this.selectContainer.length >0  && this.selectContainer.clear();
      this.selectContainer.createEmbeddedView(this.history);
    }
  }
 
  ngOnDestroy() {

  }
}
