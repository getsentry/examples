# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.views.generic.base import TemplateView

# Create your views here.


class HomePageView(TemplateView):
    template_name = "myapp/home.html"


def get_some_squares(n=3):
    reply = 'The first {} squares are: '.format(n)
    for x in range(1, n+1):
        reply += '{}, '.format(x * x)
    reply = reply.rstrip(', ') + '.'  # don't @ me
    return reply


class GoodView(TemplateView):
    template_name = "myapp/goodbad.html"

    def get_context_data(self, **kwargs):
        context = super(GoodView, self).get_context_data(**kwargs)
        context['good_or_bad'] = 'Working'
        context['body_text'] = get_some_squares(5)
        return context


class BadView(TemplateView):
    template_name = "myapp/goodbad.html"

    def get_context_data(self, **kwargs):
        context = super(BadView, self).get_context_data(**kwargs)
        context['good_or_bad'] = 'Broken'
        context['body_text'] = 'This will never be shown.'
        if True:
            raise Exception('Bad View Loaded')
        return context
