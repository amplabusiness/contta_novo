import { createContext, useContext, useReducer } from 'react';

import { IState, IAction, IUIContext } from './types';

// Action creators
const UI_VIDEO = '@ui/UI_VIDEO';

// Initial state
const initialState: IState = {
  isVideoOpen: false,
};

// Reducer
const reducer = (state = initialState, action: IAction) => {
  switch (action.type) {
    case UI_VIDEO: {
      return { ...state, isVideoOpen: action.payload };
    }

    default: {
      return state;
    }
  }
};

// Action creator
const videoAction = (payload: boolean) => ({ type: UI_VIDEO, payload });

// Context and Provider
const UIContext = createContext({} as IUIContext);

export const useUIContext = (): IUIContext => useContext(UIContext);

import type { ReactNode } from 'react';

type ProviderProps = { children?: ReactNode };

const UIProvider = ({ children }: ProviderProps) => {
  const [state, dispatch] = useReducer(reducer, initialState);

  const openVideo = () => {
    document.body.style.overflow = 'hidden';
    dispatch(videoAction(true));
  };
  const closeVideo = () => {
    document.body.style.overflow = 'auto';
    dispatch(videoAction(false));
  };

  return (
    <UIContext.Provider value={{ uiState: state, openVideo, closeVideo }}>
      {children}
    </UIContext.Provider>
  );
};

export default UIProvider;
