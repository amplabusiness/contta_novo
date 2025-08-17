export interface IState {
  isVideoOpen: boolean;
}

export interface IAction {
  type: string;
  payload: boolean;
}

export interface IUIContext {
  uiState: IState;
  openVideo: () => void;
  closeVideo: () => void;
}
