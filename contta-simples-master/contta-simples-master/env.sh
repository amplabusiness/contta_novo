#!/bin/bash
 if [ ! -z ${REACT_APP_TOKEN} ]; then
 cat <<END
 window.REACT_APP_TOKEN='${REACT_APP_TOKEN}';
END