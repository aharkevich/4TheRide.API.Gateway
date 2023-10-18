###################################
##### function to exit script #####
###################################
function exitScript()
{
  local message=$1
    echo -e $RED
    [ -z "$message" ] && message="Exited"
    echo -e "$message at ${BASH_SOURCE[1]}:${FUNCNAME[1]} line ${BASH_LINENO[0]}." >&2
    exit 1
}

####################################
##### function to build docker #####
####################################
function buildDocker()
{
   echo "==> triggering docker-compose build, please wait..."

   mkdir -p artifacts
   (
     docker-compose -f docker-compose.build.yaml build --pull --progress=plain && \
     docker-compose -f docker-compose.build.yaml up && \
     docker-compose -f docker-compose.build.yaml rm -fsv
   ) || exitScript "docker-compose operations failed, exiting..."
}

case "$1" in
  "build")
    buildDocker
    shift
    ;;
  *)
    exitScript "Must specify build as a parameter to the script, exiting..."
esac
