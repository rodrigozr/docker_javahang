# docker_javahang
Demonstration of a severe Java hang issue on Windows Containers

# How to test if the bug is fixed?
docker run --rm rodrigozr/javahang java HelloWorld

If the command above hangs, it means the bug is not fixed yet
