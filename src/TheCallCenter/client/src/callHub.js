import { HubConnectionBuilder } from "@aspnet/signalr";

class CallHub {

  constructor() {
    this.connection = new HubConnectionBuilder()
      .withUrl("/api/call-hub")
      .build();
  }

  start() {
    this.connection.start().catch(err => console.error(err.toString()));
  }

  onNewCall(callHandler) {
    this.connection.on("NewCall", callHandler);
  }
}

export default new CallHub();
